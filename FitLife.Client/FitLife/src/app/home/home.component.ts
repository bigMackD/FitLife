import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../authentication/services/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})

export class HomeComponent implements OnInit {
  isVisible: boolean = true;
  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit() {
   this.isVisible =this.authenticationService.isInRole(['Admin']);
  }

}
