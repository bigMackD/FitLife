import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatExpansionPanel, MatListOption } from '@angular/material';
import { GridPanelComponent } from '@swimlane/ngx-charts';
import { NotificationService } from '../shared/services/notification.service';
import { CategoryEnum } from './enums/category.enum';
import { GetUserMealsRequest } from './models/get-user-meals/get-user-meals.request';
import { UserMeal } from './models/get-user-meals/get-user-meals.response';
import { RegisterMealDialogComponent } from './register-meal-dialog/register-meal-dialog.component';
import { DashboardChartService, StackedHorizontalChartItem } from './services/dashboard-chart.service';
import { UserMealsService } from './services/user-meals.service';

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

    selectedMeals:number[] = [];
  
@ViewChild('matExpansionPanel', { static: true }) matExpansionPanelElement: MatExpansionPanel;

  constructor(public dialog: MatDialog,
    private notificationService: NotificationService,
    private userMealsService: UserMealsService,
    private dashboardChartService: DashboardChartService) {
     }

  ngOnInit() {
    this.getUserMealsByDate();
    this.dashboardChartService.createChartData();
    this.chartData = this.dashboardChartService.chartData;
  }

  nextDay(){
    this.incrementDay(1)
    this.getUserMealsByDate();
    this.clearSelction();
  }

  previousDay(){
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

  private handleExpansionPanel(): void{
    if(this.selectedMeals.length > 0){
      this.matExpansionPanelElement.open();
    }else{
      this.matExpansionPanelElement.close();
    }
  }

  private clearSelction(): void{
    this.selectedMeals = [];
    this.handleExpansionPanel();
  }

  registerMeal():void{
    let dialogRef = this.dialog.open(RegisterMealDialogComponent, {
      height: '150px',
      width: '600px',
      data: {
        date: this.viewDate
      }
    });

    dialogRef.afterClosed().subscribe(_ =>
     this.getUserMealsByDate() )
  }

  getUserMealsByDate():void{
    let request: GetUserMealsRequest = {
      consumedDate: this.viewDate
    }
    this.userMealsService.getByDate(request).subscribe(response =>
      {
        if (!response.success) {
          this.notificationService.error(response.errors);
        }
        else{
        this.userMeals = response.userMeals;
        this.sortMealsByCategory();
       this.dashboardChartService.updateChartData( this.breakfastMeals,
        this.lunchMeals,
        this.dinnerMeals,
        this.snackMeals)
        this.chartData = [...this.dashboardChartService.chartData];
        }
      })
  }

  sortMealsByCategory():void {
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

}
