import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessActivityListComponent } from './business-activity-list.component';

describe('BusinessActivityListComponent', () => {
  let component: BusinessActivityListComponent;
  let fixture: ComponentFixture<BusinessActivityListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusinessActivityListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusinessActivityListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
