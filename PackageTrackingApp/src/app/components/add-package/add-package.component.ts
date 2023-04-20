import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Package } from 'src/app/models/package.model';
import { PackageService } from 'src/app/services/package.service';

@Component({
  selector: 'app-add-package',
  templateUrl: './add-package.component.html',
  styleUrls: ['./add-package.component.css']
})
export class AddPackageComponent implements OnInit {
  
  addForm:FormGroup; 
  constructor(private packageService:PackageService,private router: Router) { 
    this.addForm=new FormGroup({
      'code':new FormControl('',[Validators.required, Validators.minLength(1)]),
      'pacstatus':new FormControl('',Validators.required),
    });
  }

  ngOnInit(): void {
    this.addForm=new FormGroup({
      'code':new FormControl('',[Validators.required, Validators.minLength(1)]),
      'pacstatus':new FormControl('',Validators.required),
    });
  }

  add():void
  {
     if(this.addForm.valid)
     {
        let packet:Package=new Package();
        packet.packageCode=this.addForm.value['code'];
        packet.packageStatus=this.addForm.value['pacstatus'];
        this.packageService.Add(packet).subscribe(
          (data:boolean)=>{
            if(data)
            {
              alert("Successful");
            }
            else  
            {
              alert("Error");
            }
          }
        );
     }
     else
     {
       alert("Error in input");
     }
  }
  
}