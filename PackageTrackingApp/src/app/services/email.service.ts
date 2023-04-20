import { Injectable } from '@angular/core'; 
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { Email } from '../models/email.model';

@Injectable({
    providedIn: 'root'
})

export class EmailService { 
    subscription(email:Email):Observable<boolean>{ 
        return this.http.post<boolean>(environment.ServiceUrl + '/api/emails', email); 
    }
    constructor(private http:HttpClient) {}
}