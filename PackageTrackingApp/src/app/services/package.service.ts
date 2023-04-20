import { Injectable } from '@angular/core'; 
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { Package } from '../models/package.model';

@Injectable({
  providedIn: 'root'
})
export class PackageService { 

  GetAll() :Observable<Array<Package>> { 
    return this.http.get<Array<Package>>(environment.ServiceUrl + '/api/packages'); 
   }
  Add(packet:Package) :Observable<boolean> { 
    return this.http.post<boolean>(environment.ServiceUrl + '/api/packages',packet); 
  }
  EditPackage(packet:Package):Observable<Package>{ 
    return this.http.post<Package>(environment.ServiceUrl + '/api/packages/put',packet); 
  }
  GetById(id: number|undefined) :Observable<Package> { 
    return this.http.get<Package>(environment.ServiceUrl + '/api/packages/'+id); 
   }
   constructor(private http:HttpClient) {}
}
