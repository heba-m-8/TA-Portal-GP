import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-phd-nav-menu',
  templateUrl: './phd-nav-menu.component.html',
  styleUrls: ['./phd-nav-menu.component.css']
})
export class PhdNavMenuComponent implements OnInit {
	navLink1:boolean=true;
	navLink2:boolean=false;
	constructor(private router: Router) { }

	ngOnInit(): void {	
		this.router.events.subscribe(() => {
			const url: string = this.router.url;
			if (url.indexOf('workrecord') >= 0) {
				this.navLink1 = false;
				this.navLink2 = true;
			} else {
			  // Reset if the URL doesn't match any condition
			  
			  this.navLink1 = true;
			  this.navLink2 = false;
			}
		  });	
	}

  
	logout() {
		localStorage.clear();
		this.router.navigate(['']);
	}

	goToProfile(){
		this.router.navigate(['taphd/profile']);

	}
}
