import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/_Services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  loggedIn: boolean
  constructor(private accountService: AccountService, private http: HttpClient) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  login() {
      return this.accountService.login(this.model)
        .subscribe(arg =>
          {
            console.log(arg);
            this.loggedIn = true;
          }, error => {console.log(error)}
        );
      }

  logout(){
    this.loggedIn = false;
    this.accountService.logout();
  }

  getCurrentUser(){
    this.accountService.currentUser$.subscribe(user => this.loggedIn = !!user);
  }

}
