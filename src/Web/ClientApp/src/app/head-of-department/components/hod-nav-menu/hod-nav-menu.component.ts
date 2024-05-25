import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-hod-nav-menu',
  templateUrl: './hod-nav-menu.component.html',
  styleUrls: ['./hod-nav-menu.component.css']
})
export class HodNavMenuComponent implements OnInit {
	navLink1:boolean=true;
	navLink2:boolean=false;
	navLink3:boolean=false;
	constructor(private router: Router) { 

		
	}

	ngOnInit(): void {		
		this.router.events.subscribe(() => {
			const url: string = this.router.url;
			if (url.indexOf('courses') >= 0) {
			  this.navLink1 = false;
			  this.navLink2 = true;
			  this.navLink3 = false;
			} else if (url.indexOf('workrecord') >= 0) {
			  this.navLink1 = false;
			  this.navLink2 = false;
			  this.navLink3 = true;
			} else {
			  // Reset if the URL doesn't match any condition
			  this.navLink1 = true;
			  this.navLink2 = false;
			  this.navLink3 = false;
			}
		  });
	}

  
	logout() {
		localStorage.clear();
		this.router.navigate(['']);
	}

	goToProfile(){
		this.router.navigate(['hod/profile']);

	}
}

