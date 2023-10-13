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
      name: 'Task Board',
      link: '/',
      icon: 'dashboard'
    },
    {
      name: 'List',
      link: '/',
      icon: 'event'
    },
    {
      name: 'Timeline',
      link: '/',
      icon: 'list'
    },
    {
      name: 'Calendar',
      link: '/',
      icon: 'calendar_month'
    },
    {
      name: 'Progress',
      link: '/',
      icon: 'show_chart'
    },
    {
      name: 'Forms',
      link: '/',
      icon: 'content_paste'
    }
  ];

  communicationItems: Section[] = [
    {
      name: 'Chat',
      link: '/',
      icon: 'chat'
    }
  ];

  trackByName(index: any, section: Section) {
    return section.name;
  }
}
