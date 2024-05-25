import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dean-nav-menu',
  templateUrl: './dean-nav-menu.component.html',
  styleUrls: ['./dean-nav-menu.component.css']
})
export class DeanNavMenuComponent implements OnInit {

	constructor(private router: Router) { }

	ngOnInit(): void {		
	}

  
	logout() {
		localStorage.clear();
		this.router.navigate(['']);
	}

	goToProfile(){
		this.router.navigate(['dean/profile']);

	}
}