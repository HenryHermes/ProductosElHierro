import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';

@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [NavMenuComponent],
  templateUrl: './producto.component.html',
  styleUrl: './producto.component.css'
})
export class ProductoComponent {
  
}
