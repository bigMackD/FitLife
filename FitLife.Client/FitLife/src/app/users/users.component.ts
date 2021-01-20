import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator } from '@angular/material';
import { tap } from 'rxjs/operators';
import { NotificationService } from '../shared/services/notification.service';
import { UsersDataSource } from './datasource/users.datsource';
import { DisableUserRequest } from './models/disable/disable.request';
import { DisableUserResponse } from './models/disable/disable.response';
import { EnableUserRequest } from './models/enable/enable.request';
import { EnableUserResponse } from './models/enable/enable.response';
import { UsersService } from './services/users.service';
import { UserDialogComponent } from './user-dialog/user-dialog.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.sass']
})
export class UsersComponent implements OnInit {

  dataSource: UsersDataSource;
  displayedColumns= [ "name", "email", "options"];

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  constructor(private usersService:UsersService, public dialog: MatDialog,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.dataSource = new UsersDataSource(this.usersService);
    this.dataSource.loadUsers();
  }

  ngAfterViewInit() {
    this.paginator.page
        .pipe(
            tap(() => this.loadUsersPage())
        )
        .subscribe();
}

loadUsersPage() {
  this.dataSource.loadUsers(
      '',
      'asc',
      this.paginator.pageIndex,
      this.paginator.pageSize);
}

details(userId: string){
  let dialogRef = this.dialog.open(UserDialogComponent, {
    height: '400px',
    width: '600px',
    data: {
      id: userId
    }
  });
}

enable(userId:string): void{
  var request:EnableUserRequest = {
    id: userId
  };
  this.usersService.enable(request).subscribe(response =>
    this.handleEnableResponse(response));
}

handleEnableResponse(response: EnableUserResponse){
  if (!response.success) {
    this.notificationService.error(response.errors);
  }
  else {
    this.notificationService.success('User account succesfully enabled!');
    this.loadUsersPage();
  }
}

disable(userId:string): void{
  var request:DisableUserRequest = {
    id: userId
  };
  this.usersService.disable(request).subscribe(response =>
    this.handleDisabledResponse(response));
}

handleDisabledResponse(response: DisableUserResponse){
  if (!response.success) {
    this.notificationService.error(response.errors);
  }
  else {
    this.notificationService.success('User account succesfully disabled!');
    this.loadUsersPage();
  }
}

}
