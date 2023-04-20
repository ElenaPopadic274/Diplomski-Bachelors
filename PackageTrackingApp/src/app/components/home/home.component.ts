import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt'; 
import { FormControl, FormGroup, Validators  } from '@angular/forms';
import { PackageService } from 'src/app/services/package.service'; 
import { Package } from 'src/app/models/package.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
 
  trackerForm:FormGroup;
  packets: Package|null=null;
  invalidNumber:boolean=false;
  guser:object=new Object();

  isActivated:string="";
  status:string="";
  
  constructor(private router: Router, private helper:JwtHelperService, private packageService:PackageService){
    let token = localStorage.getItem("token");  
    if(token!=null&& !this.helper.isTokenExpired(token))
    {
         let user=helper.decodeToken(token);
         this.isActivated=user.isActivated;
         this.status=user.Status;
     }

    this.trackerForm = new FormGroup({
      'code':new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(10)])
    });
  }
 

  ngOnInit(): void {
    this.trackerForm=new FormGroup({
      'code':new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(10)])
    })
  }

  track():void
  {
    let id=this.trackerForm.value['code'];
    this.packageService.GetById(id).subscribe(
      (data:Package)=>{ 
        this.invalidNumber=data==null;
        this.packets=data;

      }
    )
  }

}
