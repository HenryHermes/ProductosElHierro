import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';
import { LoginData } from '../Interfaces/login-data';
import { FormsModule } from '@angular/forms';
import { dateTimestampProvider } from 'rxjs/internal/scheduler/dateTimestampProvider';
import { ApisService } from '../Services/apis.service';
import { LoginResponse } from '../Interfaces/login-response';

@Component({
  selector: 'app-loging',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './loging.component.html',
  styleUrl: './loging.component.css'
})
export class LogingComponent {
  constructor( private router: Router, private User: ApisService ) { }
  
  UserName:string = ''
  Password:string = ''

  ChangePage(IsUser: boolean){
    if (IsUser==true) {
      this.router.navigate(['../Producto'])
    }
    
  }

  GetFormData(Data : LoginData){
    this.UserName=Data.userName
    this.Password=Data.password
    
    this.User.LoginUsers(Data).subscribe((Result : LoginResponse) => {
      this.ChangePage(Result.success)
      localStorage.setItem('JWTToken', Result.message)
    })

  }
}
