import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator } from '@angular/material';
import { tap } from 'rxjs/operators';
import { UsersDataSource } from './datasource/users.datsource';
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

  constructor(private usersService:UsersService, public dialog: MatDialog) { }

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

}
