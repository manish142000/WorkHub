import { Component, AfterContentInit, ContentChildren, QueryList } from '@angular/core';
import { TabsComponent } from '../tabs/tabs.component';

@Component({
  selector: 'app-tabscontainer',
  templateUrl: './tabscontainer.component.html',
  styleUrls: ['./tabscontainer.component.css']
})
export class TabscontainerComponent implements AfterContentInit{
  @ContentChildren(TabsComponent) tabs : QueryList<TabsComponent>  = new QueryList();

  ngAfterContentInit(){
    if( this.tabs.first ){
      this.setActive(this.tabs.first);
    }
  }

  setActive( tab : TabsComponent ){
    this.tabs.filter( tab => {
      tab.active = false;
    });

    tab.active = true;
  }
}
