import { Component, OnInit } from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material';
import { Inject } from '@angular/core';
import { UsersService } from '../services/users.service';
import { UserDetailsRequest } from '../models/details/userDetails.request';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.sass']
})
export class UserDialogComponent implements OnInit {

detailsForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data:any,
  private usersService:UsersService, private fb:FormBuilder) { }

  ngOnInit() {
    this.constructForm();
    this.setFormValues();
  }

  private constructForm(): void {
    this.detailsForm = this.fb.group({
      username: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      email: ['', [Validators.required]],
      fullName: ['', [Validators.required]],
      phoneNumberConfirmed: ['', [Validators.required]],
      twoFactorEnabled: ['', [Validators.required]],
    });
  }

  private setFormValues():void{
    var request:UserDetailsRequest = {
      id: this.data.id
    }
    this.usersService.getDetails(request).subscribe(response =>
    {
      this.detailsForm.controls['username'].setValue(response.userName);
      this.detailsForm.controls['phoneNumber'].setValue(response.phoneNumber);
      this.detailsForm.controls['email'].setValue(response.email);
      this.detailsForm.controls['fullName'].setValue(response.fullName);
      this.detailsForm.controls['phoneNumberConfirmed'].setValue(response.phoneNumberConfirmed);
      this.detailsForm.controls['twoFactorConfirmed'].setValue(response.twoFactorEnabled);


    })
  }

}
