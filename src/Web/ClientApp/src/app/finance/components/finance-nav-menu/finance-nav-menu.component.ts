import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-finance-nav-menu',
  templateUrl: './finance-nav-menu.component.html',
  styleUrls: ['./finance-nav-menu.component.css']
})
export class FinanceNavMenuComponent implements OnInit {

	constructor(private router: Router) { }

	ngOnInit(): void {		
	}

  
	logout() {
		localStorage.clear();
		this.router.navigate(['']);
	}

	goToProfile(){
		this.router.navigate(['finance/profile']);

	}
}
