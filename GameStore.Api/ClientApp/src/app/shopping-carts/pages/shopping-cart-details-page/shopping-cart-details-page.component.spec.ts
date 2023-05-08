import {ComponentFixture, TestBed} from '@angular/core/testing';

import {ShoppingCartDetailsPageComponent} from './shopping-cart-details-page.component';

describe('ShoppingCartDetailsPageComponent', () => {
    let component: ShoppingCartDetailsPageComponent;
    let fixture: ComponentFixture<ShoppingCartDetailsPageComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ShoppingCartDetailsPageComponent]
        })
            .compileComponents();

        fixture = TestBed.createComponent(ShoppingCartDetailsPageComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
