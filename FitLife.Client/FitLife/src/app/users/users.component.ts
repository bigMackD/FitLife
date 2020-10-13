import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material';
import { tap } from 'rxjs/operators';
import { UsersDataSource } from './datasource/users.datsource';
import { UsersService } from './services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.sass']
})
export class UsersComponent implements OnInit {

  dataSource: UsersDataSource;
  displayedColumns= [ "name", "email", "options"];

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  constructor(private usersService:UsersService) { }

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
  console.log(this.paginator)
  this.dataSource.loadUsers(
      '',
      'asc',
      this.paginator.pageIndex,
      this.paginator.pageSize);
}

}
