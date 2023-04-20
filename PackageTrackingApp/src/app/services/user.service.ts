import { Injectable } from '@angular/core'; 
import { Observable } from 'rxjs'; 
import { environment } from 'src/environments/environment'; 
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Token } from '../models/token.model';
import { Login } from '../models/login.model';
import { User } from '../models/user.model';
import { url } from '../models/url.model';
   
@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  constructor(private http:HttpClient) {
  }
  GetUndelivered():Observable<Array<User>>{
    return this.http.get<Array<User>>(environment.ServiceUrl + '/api/users/Unactivated');
  }
  GetUser(username: string|null):Observable<User>{ 
   return this.http.get<User>(environment.ServiceUrl + '/api/users/username/'+username);
  }
  GetUserById(id: number|undefined):Observable<User>{ 
    return this.http.get<User>(environment.ServiceUrl + '/api/users/'+id);
  }
  registration(user: User) :Observable<boolean>{ 
    return this.http.post<boolean>(environment.ServiceUrl + '/api/users', user); 
  }
  verifyUser(userid: number, response:boolean) :Observable<boolean>{ 
    if(response)
      return this.http.post<boolean>(environment.ServiceUrl + '/api/users/verifyUser', userid); 
    return this.http.post<boolean>(environment.ServiceUrl + '/api/users/dismissUser',userid); 
  }
  login(login:Login) :Observable<Token> { 
   return this.http.post<Token>(environment.ServiceUrl + '/api/users/login', login); 
  } 
  EditUser(user:User):Observable<Token>{ 
    return this.http.post<Token>(environment.ServiceUrl + '/api/users/put', user); 
  }
}

