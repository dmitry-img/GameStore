import {ComponentFixture, TestBed} from '@angular/core/testing';

import {PublisherDetailsPageComponent} from './publisher-details-page.component';

describe('PublisherDetailsPageComponent', () => {
    let component: PublisherDetailsPageComponent;
    let fixture: ComponentFixture<PublisherDetailsPageComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [PublisherDetailsPageComponent]
        })
            .compileComponents();

        fixture = TestBed.createComponent(PublisherDetailsPageComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
