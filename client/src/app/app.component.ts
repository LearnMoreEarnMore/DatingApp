import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/_Models/user';
import { AccountService } from 'src/_Services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Welcome to Dating Application';
  users : any;
  constructor(private accountService : AccountService) {

  }
  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user : User = JSON.parse(localStorage.getItem('user'));
    this.accountService.getCurrentUser(user);
  }
}
