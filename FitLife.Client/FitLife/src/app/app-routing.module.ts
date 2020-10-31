import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './authentication/register/register.component';
import { LoginComponent } from './authentication/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { UsersComponent } from './users/users.component';
import { NutritionComponent } from './nutrition/nutrition.component';


const routes: Routes = [
  { path:'', redirectTo:'register', pathMatch:'full' },
  { path:'register',component: RegisterComponent },
  { path:'login',component: LoginComponent },
  { path:'home',component: HomeComponent, canActivate:[AuthGuard] },
  { path:'users',component: UsersComponent, canActivate:[AuthGuard],
   data:{permittedRoles:['Admin']} },
  { path:'nutrition',component: NutritionComponent, canActivate:[AuthGuard] },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
