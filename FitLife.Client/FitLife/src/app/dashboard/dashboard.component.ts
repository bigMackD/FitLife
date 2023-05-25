import { ChangeDetectorRef, Component, EventEmitter, NgZone, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog, MatExpansionPanel, MatListOption } from '@angular/material';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Guid } from 'guid-typescript';
import { CalculatePeriodicDietRequest } from '../processor/models/calculatePeriodicDiet.request';
import { NotificationService } from '../shared/services/notification.service';
import { ProcessorService } from '../shared/services/processor.service';
import { CategoryEnum } from './enums/category.enum';
import { DeleteUserMealsRequest } from './models/delete-user-meals/delete-user-meals.request';
import { GetUserMealsRequest } from './models/get-user-meals/get-user-meals.request';
import { UserMeal } from './models/get-user-meals/get-user-meals.response';
import { RegisterMealDialogComponent } from './register-meal-dialog/register-meal-dialog.component';
import { DashboardChartService, StackedHorizontalChartItem } from './services/dashboard-chart.service';
import { UserMealsService } from './services/user-meals.service';
import { ProgressBarService } from '../shared/services/progress-bar.service';
import { DownloadService } from '../shared/services/download.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})



export class DashboardComponent implements OnInit {
  viewDate = new Date();
  userMeals: UserMeal[];
  categoryEnum = CategoryEnum;
  breakfastMeals: UserMeal[] = [];
  lunchMeals: UserMeal[] = [];
  dinnerMeals: UserMeal[] = [];
  snackMeals: UserMeal[] = [];
  first: MatExpansionPanel;
  // chart options
  showXAxis: boolean = true;
  showYAxis: boolean = true;
  gradient: boolean = false;
  showLegend: boolean = true;
  showXAxisLabel: boolean = true;
  yAxisLabel: string = 'Meal';
  showYAxisLabel: boolean = true;
  xAxisLabel: string = 'Macronutrients';
  chartData: StackedHorizontalChartItem[] = [];
  view: any[] = [700, 400];
  colorScheme = {
    domain: ['#9B26AF', '#691A99', '#68EFAD', '#AAAAAA']
  };

  selectedMeals: number[] = [];
  private hubConnectionBuilder!: HubConnection;
  messages: any[] = [];
  private eventId: Guid;

  @ViewChild('matExpansionPanel', { static: true }) matExpansionPanelElement: MatExpansionPanel;

  @Output()
  isProgressVisible: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(public dialog: MatDialog,
    private notificationService: NotificationService,
    private userMealsService: UserMealsService,
    private dashboardChartService: DashboardChartService,
    private processorService: ProcessorService,
    private progressBarService: ProgressBarService,
    private ngZone: NgZone,
    private downloadSerivce: DownloadService) {
  }

  ngOnInit() {
    this.getUserMealsByDate();
    this.dashboardChartService.createChartData();
    this.chartData = this.dashboardChartService.chartData;
    this.handleExpansionPanel();

    this.hubConnectionBuilder = new HubConnectionBuilder().withUrl('https://localhost:44326/processor').configureLogging(LogLevel.Information).build();
    this.hubConnectionBuilder.start().then(() => console.log('Connection started.......!'))
      .catch(err => {
        console.log(`Error while connect with server ${err}`);
        this.notificationService.error(['Error occured while connecting to processor hub!']);
        this.progressBarService.hide();
      });
    this.ngZone.run(() => {
      this.hubConnectionBuilder.on('Notify', (ids: string[]) => {
        console.log(ids);
        this.messages = [...this.messages, ids];
        this.notificationService.dismiss();
        this.notificationService.success('Success');
        this.progressBarService.hide();
        this.downloadSerivce.download(ids[0]);
      });
    });
  }

  nextDay() {
    this.incrementDay(1)
    this.getUserMealsByDate();
    this.clearSelction();
  }

  previousDay() {
    this.incrementDay(-1)
    this.getUserMealsByDate();
    this.clearSelction();
  }

  private incrementDay(delta: number): void {
    this.viewDate = new Date(
      this.viewDate.getFullYear(),
      this.viewDate.getMonth(),
      this.viewDate.getDate() + delta);
  }

  onGroupsChange(options: MatListOption[]): void {
    this.selectedMeals = options.map(o => o.value.id);
    this.handleExpansionPanel();
  }

  private handleExpansionPanel(): void {
    if (this.selectedMeals.length > 0) {
      this.matExpansionPanelElement.disabled = false;
      this.matExpansionPanelElement.open();
    } else {
      this.matExpansionPanelElement.close();
      this.matExpansionPanelElement.disabled = true;
    }
  }

  private clearSelction(): void {
    this.selectedMeals = [];
    this.handleExpansionPanel();
  }

  registerMeal(): void {
    let dialogRef = this.dialog.open(RegisterMealDialogComponent, {
      height: '150px',
      width: '600px',
      data: {
        date: this.viewDate
      }
    });

    dialogRef.afterClosed().subscribe(_ =>
      this.getUserMealsByDate())
  }

  getUserMealsByDate(): void {
    let request: GetUserMealsRequest = {
      consumedDate: this.viewDate
    }
    this.userMealsService.getByDate(request).subscribe(response => {
      if (!response.success) {
        this.notificationService.error(response.errors);
      }
      else {
        this.userMeals = response.userMeals;
        this.sortMealsByCategory();
        this.dashboardChartService.updateChartData(this.breakfastMeals,
          this.lunchMeals,
          this.dinnerMeals,
          this.snackMeals)
        this.chartData = [...this.dashboardChartService.chartData];
      }
    })
  }

  deleteUserMeals() {
    const request = new DeleteUserMealsRequest(this.selectedMeals);
    this.userMealsService.delete(request).subscribe(response => {
      if (response.success) {
        this.notificationService.success("Meal was successfully deleted!");
        this.getUserMealsByDate();
      }
    })
  }

  sortMealsByCategory(): void {
    this.breakfastMeals = [];
    this.lunchMeals = [];
    this.dinnerMeals = [];
    this.snackMeals = [];
    this.userMeals.map(meal => {
      switch (meal.categoryId) {
        case CategoryEnum.Breakfast:
          this.breakfastMeals.push(meal);
          break;
        case CategoryEnum.Lunch:
          this.lunchMeals.push(meal);
          break;
        case CategoryEnum.Dinner:
          this.dinnerMeals.push(meal);
          break;
        case CategoryEnum.Snacks:
          this.snackMeals.push(meal);
          break;
        default:
          break;
      }
    })
  }

  calculatePeriodicDiet(): void {
    this.eventId = Guid.create();
    let request = new CalculatePeriodicDietRequest(this.eventId);
    this.progressBarService.show();
    this.processorService.calculatePeriodicDiet(request).subscribe();
    this.notificationService.progress('Calulating diets in progress...');
  }
}
