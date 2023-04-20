import { Package } from './../../models/package.model';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router'; 
import { PackageService } from 'src/app/services/package.service';

@Component({
  selector: 'app-package-status',
  templateUrl: './package-status.component.html',
  styleUrls: ['./package-status.component.css']
})
export class PackageStatusComponent implements OnInit {

  statusForm:FormGroup;
  constructor(private packageService:PackageService, private router:Router ) { 
    this.statusForm=new FormGroup({
      'pacstatus':new FormControl('',Validators.required),
      'packageCode':new FormControl('',Validators.required)
    });

  } 

  ngOnInit(): void {
    this.statusForm=new FormGroup({
      'pacstatus':new FormControl('',Validators.required),
      'packageCode':new FormControl('',Validators.required)
    });
  }

  status():void
  {
    if(this.statusForm.valid)
    {
      let packet:Package=new Package();
      packet.packageCode = this.statusForm.value['packageCode'];
      packet.packageStatus=this.statusForm.value['pacstatus'];
      this.packageService.EditPackage(packet).subscribe(
        (data:Object)=>{
          if(data)
          {
            alert("Successful");
          }
          else  
          {
            alert("");
          }
        }
      );
    }
  }

}
