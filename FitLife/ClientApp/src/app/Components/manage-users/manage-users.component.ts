import { User } from './../../Models/user';
import { AccountService } from './../../services/Account/account.service';
import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-manage-users',
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.css']
})
export class ManageUsersComponent implements OnInit {
userList: User[] = [];

  constructor(
    private accountService: AccountService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
    this.getUsers()
  }
  getUsers() {
    this.accountService
      .getUsers()
      .subscribe(result => {
        result.forEach(element => {
          var user = new User(element.Id,element.UserName,element.Email);
          this.userList.push(user);
        });
      }, error => {
        console.log(error);
      });
  }

  Remove(user: User){
    const index: number = this.userList.indexOf(user);
    if (index !== -1) {
      this.userList.splice(index,1);
    }
  }
}
