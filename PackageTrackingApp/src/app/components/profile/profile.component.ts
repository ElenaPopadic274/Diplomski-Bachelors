import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router'; 
import { UserService } from 'src/app/services/user.service';
import jwt_decode from "jwt-decode";
import { Token } from '@angular/compiler';
import { TokenData } from 'src/app/models/token.model';
import { User } from 'src/app/models/user.model';
import {JwtHelperService} from '@auth0/angular-jwt';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileForm:FormGroup;
  user:User=new User();
  isGoogle:string="";
  constructor(private userService:UserService,private router: Router, private helper:JwtHelperService,private sanitizer: DomSanitizer) {  
      
    this.profileForm= new FormGroup({
      'username':new FormControl('',[Validators.required, Validators.minLength(4)]),
      'password':new FormControl('',[Validators.required, Validators.minLength(4)]),
      'firstname':new FormControl('',[Validators.required, Validators.minLength(2)]),
      'lastname':new FormControl('',[Validators.required, Validators.minLength(2)])
    });  
 
    let tokens = localStorage.getItem("token"); 
    if(tokens==null)
      return;
    let token=this.helper.decodeToken(tokens);
    this.isGoogle=token.isGoogle;
    this.userService.GetUser((token as TokenData).username).subscribe(
      (data : User) => {  
        this.user=data;
        this.profileForm= new FormGroup({
          'username':new FormControl(this.user?.username,[Validators.required, Validators.minLength(4)]),
          'password':new FormControl(''),
          'firstname':new FormControl(this.user?.firstname,[Validators.required, Validators.minLength(2)]),
          'lastname':new FormControl(this.user?.lastname,[Validators.required, Validators.minLength(2)]),  
        });
      }
    ); 

  }

  ngOnInit(): void {
  }
  onFileChange(event:Event):void
  {  
    const input = event.target as HTMLInputElement;

    if (!input.files?.length) {
        return;
    }

    const file = input.files[0]; 
    const formData = new FormData();
    formData.append('file', file, file.name);

  }
  change():void
  {
    if(this.profileForm.valid)
    {
      let user:User=new User();
      user.username=this.profileForm.value['username'];
      user.password=this.profileForm.value['password'];
      user.firstname=this.profileForm.value['firstname'];
      user.lastname=this.profileForm.value['lastname'];
      user.id=this.user.id;
      user.type=this.user.type;
      this.userService.EditUser(user).subscribe(
        (data : Object) => { 
          alert("Successfuly changed data.");
          
      },
      error => {
        alert("Error in communication with server.");
      }
        
      );
    }
    
  }   
}
 