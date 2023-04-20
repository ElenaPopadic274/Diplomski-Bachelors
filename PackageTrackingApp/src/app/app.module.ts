import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './components/nav/nav.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component'; 
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RegistrationComponent } from './components/registration/registration.component';
import { ProfileComponent } from './components/profile/profile.component';
import { UserService } from './services/user.service';
import { PackageService } from './services/package.service'; 
import { ToastrModule } from 'ngx-toastr';
import { CountdownModule } from 'ngx-countdown';
import { JwtModule } from "@auth0/angular-jwt";
import { environment } from 'src/environments/environment';
import { VerificationComponent } from './components/verification/verification.component';
import { AddPackageComponent } from './components/add-package/add-package.component';
import { PackageStatusComponent } from './components/package-status/package-status.component';
import { AllPackagesComponent } from './components/all-packages/all-packages.component';
import { SubscriptionComponent } from './components/subscription/subscription.component';
import { EmailService} from './services/email.service'; 

export function tokenGetter() {
  return localStorage.getItem("token");
}
@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    HomeComponent,
    RegistrationComponent,
    ProfileComponent,
    VerificationComponent,
    AddPackageComponent,
    PackageStatusComponent,
    AllPackagesComponent,
    SubscriptionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    CountdownModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains:environment.allowedDomains
      }
      })
  ],
  providers: [
    UserService, 
    PackageService,
    EmailService
    ],
   
  bootstrap: [AppComponent]
})
export class AppModule { }
