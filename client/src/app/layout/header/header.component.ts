import { Component } from '@angular/core';
import {MatIcon} from '@angular/material/icon';
import {MatButton} from '@angular/material/button';
import {MatBadge} from '@angular/material/badge';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { MatDialog } from '@angular/material/dialog';
import { NotifyMeDialogComponent } from '../../features/shop/notify-me-dialog/notify-me-dialog.component';

@Component({
  selector: 'app-header',
  standalone:true,
  imports: [
    MatIcon,
    MatButton,
    MatBadge,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
constructor(private dialog: MatDialog) {}

openNotifyMe() {
  this.dialog.open(NotifyMeDialogComponent, {
    width: '900px'
  });
}
}


