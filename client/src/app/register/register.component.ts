import { Component, Input, OnInit, Output , EventEmitter} from '@angular/core';
import { AccountService } from 'src/_Services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  constructor(private http: AccountService) { }

  ngOnInit(): void {
  }

  register(model : any) {
    console.log('==>' + model);
    this.http.registerUser(this.model).subscribe(response =>
      {
       console.log(response);
      }), error => {
        console.log(error);
      };
      this.cancel();
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
