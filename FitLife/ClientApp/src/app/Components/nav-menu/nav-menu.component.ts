import { AccountService } from './../../services/Account/account.service';
import { Component } from '@angular/core';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(private accountService: AccountService){}
}
