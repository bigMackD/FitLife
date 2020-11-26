import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './authentication/register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule, MatMenuModule, MatProgressSpinnerModule, MatSnackBarModule } from '@angular/material';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './authentication/login/login.component';
import { HomeComponent } from './home/home.component';
import { UsersComponent } from './users/users.component';
import { TokenInterceptor } from './authentication/interceptors/token.interceptor';
import { LoaderComponent } from './shared/loader/loader/loader.component';
import { LoaderInterceptor } from './shared/loader/loader.interceptor';
import { UserDialogComponent } from './users/user-dialog/user-dialog.component';
import { NutritionComponent } from './nutrition/nutrition.component';
import { ProductsComponent } from './nutrition/products/products.component';
import { MealsComponent } from './nutrition/meals/meals.component';
import { ProductDetailsComponent } from './nutrition/products/product-details/product-details.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    UsersComponent,
    LoaderComponent,
    UserDialogComponent,
    NutritionComponent,
    ProductsComponent,
    MealsComponent,
    ProductDetailsComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AngularMaterialModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    HttpClientModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatMenuModule,
    NgxChartsModule

  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent],
  entryComponents: [ UserDialogComponent, ]
})
export class AppModule { }
