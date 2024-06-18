import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loging',
  standalone: true,
  imports: [],
  templateUrl: './loging.component.html',
  styleUrl: './loging.component.css'
})
export class LogingComponent {
  constructor( private router: Router ) { }
  
  UserName:string = ''
  Password:string = ''

  GetUsername(v:string){
    this.UserName=v
  }
  GetPassword(v:string){
    this.Password=v
  }

  ChangePage(){
    if (this.UserName=='Hermes' && this.Password=='12345') {
      this.router.navigate(['../Producto'])
    }
    
  }
}
