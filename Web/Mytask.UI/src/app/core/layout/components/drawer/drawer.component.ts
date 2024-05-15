import { Component } from '@angular/core';

export interface Section {
  name: string,
  link: string,
  icon: string
}

@Component({
  selector: 'app-drawer',
  templateUrl: './drawer.component.html',
  styleUrls: ['./drawer.component.scss']
})
export class DrawerComponent {
  menuItems: Section[] = [
    {
      name: 'Доска',
      link: '/board',
      icon: 'dashboard'
    },
    {
      name: 'Календарь',
      link: '/calendar',
      icon: 'calendar_month'
    },
    {
      name: 'Настройки',
      link: '/settings',
      icon: 'settings'
    }
  ];

  // communicationItems: Section[] = [
  //   {
  //     name: 'Chat',
  //     link: '/',
  //     icon: 'chat'
  //   }
  // ];

  trackByName(index: any, section: Section) {
    return section.name;
  }
}
