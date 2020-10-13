import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import { BehaviorSubject, Observable, of } from 'rxjs';
import { User } from '../models/users.response';
import { UsersService } from '../services/users.service';
import {catchError, finalize } from 'rxjs/operators/'; 
import { UsersRequest } from '../models/users.request';

export class UsersDataSource implements DataSource<User> {

    private usersSubject = new BehaviorSubject<User[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();
    public count: number;

    constructor(private usersService: UsersService) {}

    connect(collectionViewer: CollectionViewer): Observable<User[]> {
     return this.usersSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
      this.usersSubject.complete();
      this.loadingSubject.complete();
    }
  
    loadUsers(filter: string = '',
                sortDirection: string = 'asc', pageIndex: number = 0, pageSize: number = 3) {
                  const request: UsersRequest = {
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    sortDirection: sortDirection
                  }
     this.usersService.getUsers(request)
     .pipe(
        finalize(() => this.loadingSubject.next(false)))
    .subscribe(response =>{
      this.count = response.count;
      this.usersSubject.next(response.users);
    }); 
    }  
}