import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-erorr404',
  templateUrl: './erorr404.component.html',
  styleUrls: ['./erorr404.component.css']
})
export class Erorr404Component implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  goToLogin(){
    this.router.navigate(['']);
  }
}