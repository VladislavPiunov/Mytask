import { Component } from '@angular/core';
import * as moment from 'moment';
import { DayOfWeek } from './model/day-of-week.model';
import { Meeting } from './model/meeting.model';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent {
  days: (Date | null)[];
  daysOfWeek = [ 
    new DayOfWeek("Понедельник","Пн"),
    new DayOfWeek("Вторник","Вт"),
    new DayOfWeek("Среда","Ср"),
    new DayOfWeek("Четверг","Чт"),
    new DayOfWeek("Пятница","Пт"),
    new DayOfWeek("Суббота","Сб"),
    new DayOfWeek("Воскресенье","Вс")
  ];

  meetings = [ new Meeting("Встреча 1", new Date(moment.now()), "1") ];

  constructor() {
    const currDate = new Date(moment.now());
    this.days = this.getDaysInMonth(currDate.getMonth(), currDate.getFullYear());
    let residual = 42 - this.days.length;

    const firstDayWeekNumber = this.daysOfWeek.findIndex(x => x.fullName.toLowerCase() == this.days[0]?.toLocaleString("ru", {weekday: "long"}));

    for (let i = 0; i < firstDayWeekNumber; i++) {
      this.days.unshift(null);
      residual = residual--;
    }

    if (residual != 0) {
      for (let i = 0; i < residual; i++) {
        this.days.push(null);
      }
    }
  }

  getDaysInMonth(month: number, year: number): Date[] {
    const date = new Date(year, month, 1);
    const days = [];
    while (date.getMonth() === month) {
      days.push(new Date(date));
      date.setDate(date.getDate() + 1);
    }
    return days;
  }
}