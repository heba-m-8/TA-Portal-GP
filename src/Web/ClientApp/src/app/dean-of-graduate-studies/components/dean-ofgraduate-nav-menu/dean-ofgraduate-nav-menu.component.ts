import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dean-ofgraduate-nav-menu',
  templateUrl: './dean-ofgraduate-nav-menu.component.html',
  styleUrls: ['./dean-ofgraduate-nav-menu.component.css']
})
export class DeanOFGraduateNavMenuComponent implements OnInit {

	constructor(private router: Router) { }

	ngOnInit(): void {		
	}

  
	logout() {
		localStorage.clear();
		this.router.navigate(['']);
	}

	goToProfile(){
		this.router.navigate(['deanofgraduatestudies/profile']);

	}
}
