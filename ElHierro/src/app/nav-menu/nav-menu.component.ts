import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {
  constructor( private router: Router ) { }

  ChangePageUsuario(){
    this.router.navigate(['../Usuarios'])
  }

  ChangePageProducto(){
    this.router.navigate(['../Producto'])
  }

  ChangePageCrearProducto(){
    this.router.navigate(['../CrearProducto'])
  }

  ChangePageCrearUsuario(){
    this.router.navigate(['../CrearUsuarios'])
  }
}
