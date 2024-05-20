import { Component, OnInit } from '@angular/core';
import { DemoTuan5Service } from '../services/demo-tuan5.service';

@Component({
  selector: 'lib-demo-tuan5',
  template: ` <p>demo-tuan5 works!</p> `,
  styles: [],
})
export class DemoTuan5Component implements OnInit {
  constructor(private service: DemoTuan5Service) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
