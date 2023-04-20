import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Email } from 'src/app/models/email.model';
import { EmailService } from 'src/app/services/email.service'; 

@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.css']
})
export class SubscriptionComponent implements OnInit {
  subForm:FormGroup;
  constructor(private emailService:EmailService, private router:Router) { 
    this.subForm=new FormGroup({
      'packageCode':new FormControl('',Validators.required),
      'email':new FormControl('',[Validators.required, Validators.email])
    })
  }

  ngOnInit(): void {
    this.subForm=new FormGroup({
      'packageCode':new FormControl('',Validators.required),
      'email':new FormControl('',[Validators.required, Validators.email])
    });
  }

  subscription():void
  {
    if(this.subForm.valid)
    {
      let email=new Email();
      email.packageId=this.subForm.value['packageCode'];
      email.email=this.subForm.value['email'];
      this.emailService.subscription(email).subscribe(
        (data:boolean) => {
          if(data)
          {
            alert("Successful subscription");
          }
          else
          {
            alert("Mail is already subscribe");
          }
        }, 
        error=> {
          alert("Error in input");
        }
      );
    }
    else
    {
      alert("There has been a mistake");
    } 
  }
}
