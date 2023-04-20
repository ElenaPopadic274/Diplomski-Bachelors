import { Component, OnInit } from '@angular/core';
import { Package } from 'src/app/models/package.model';
import { PackageService } from 'src/app/services/package.service';

@Component({
  selector: 'app-all-packages',
  templateUrl: './all-packages.component.html',
  styleUrls: ['./all-packages.component.css']
})
export class AllPackagesComponent implements OnInit {
  packets: Array<Package>=new Array<Package>();
  constructor(private packageService:PackageService) {
    this.packageService.GetAll().subscribe(
      (data: Array<Package>) => {
        this.packets=data;
      }
    )
   }

  ngOnInit(): void {
  }

}
