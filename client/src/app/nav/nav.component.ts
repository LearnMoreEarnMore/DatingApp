import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/_Models/user';
import { AccountService } from 'src/_Services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  currentUser$: Observable<User>;
  constructor(public accountService: AccountService, private http: HttpClient) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
      return this.accountService.login(this.model)
        .subscribe(arg =>
          {
            console.log(arg);
          }, error => {console.log(error)}
        );
      }

  logout(){
    this.accountService.logout();
  }
}
