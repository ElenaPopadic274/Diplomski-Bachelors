import { SubscriptionComponent } from './components/subscription/subscription.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component'; 
import { LoginGuard } from './guards/login.guard';
import { UserGuard } from './guards/user.guard';
import { RegistrationComponent } from './components/registration/registration.component';
import { ProfileComponent } from './components/profile/profile.component';
import { VerificationComponent } from './components/verification/verification.component';
import { LoggedInGuard } from './guards/logged-in.guard';
import { AdminGuard } from './guards/admin.guard';
import { DelivererGuard } from './guards/deliverer.guard';
import { AddPackageComponent } from './components/add-package/add-package.component';
import { PackageStatusComponent} from './components/package-status/package-status.component';
import { AllPackagesComponent } from './components/all-packages/all-packages.component';
import { AddelGuard } from './guards/addel.guard';

const routes: Routes = [
  { path:"home", component: HomeComponent},
  { path:"login",component:  LoginComponent, canActivate:[LoginGuard]},
  { path:"registration",component:  RegistrationComponent, canActivate:[LoginGuard]},
  { path:"profile",component:  ProfileComponent, canActivate:[LoggedInGuard]},
  { path:"verification",component:  VerificationComponent, canActivate:[AdminGuard]},
  { path:"addpackage",component:  AddPackageComponent, canActivate:[AdminGuard]},
  { path:"packagestatus", component: PackageStatusComponent, canActivate:[AddelGuard]},
  { path:"allpackages", component: AllPackagesComponent, canActivate:[AddelGuard]},
  { path:"subscription", component: SubscriptionComponent, canActivate:[LoggedInGuard,UserGuard]},
  { path:"", component:  HomeComponent} 


]; 
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
 