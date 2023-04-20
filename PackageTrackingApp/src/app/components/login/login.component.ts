import { Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators  } from '@angular/forms';
import { Router } from '@angular/router';    
import { Token } from 'src/app/models/token.model';
import { UserService } from 'src/app/services/user.service'; 
import { Login } from 'src/app/models/login.model'; 
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit { 
  
  loginForm:FormGroup; 
  guser:object=new Object();
  constructor(private userService:UserService,private router: Router) { 
    this.loginForm=new FormGroup({
      'username':new FormControl('',Validators.required),
      'password':new FormControl('',[Validators.required, Validators.minLength(4)])
    });

  
  }
  ngOnInit(): void {
    this.loginForm=new FormGroup({
      'username':new FormControl('',Validators.required),
      'password':new FormControl('',[Validators.required, Validators.minLength(4)])
    });
  } 
  user:User=new User();

  login():void
  { 
    let login=new Login();
    login.username=this.loginForm.value['username'];
    login.password=this.loginForm.value['password'];
    this.userService.login(login).subscribe(
      (data : Token) => { 
        if(data==null)
        {

          alert('Wrong username or password!');
        }
        else
        {
        localStorage.setItem('token', data.token);
        this.router.navigateByUrl('/');
        }
      },
      error => {
        alert('Server error!');
      }
    );
  } 
}
