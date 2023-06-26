import {Component, OnInit} from '@angular/core';
import {Tweet} from "../../../models/Tweet";

@Component({
  selector: 'app-twitter-updates',
  templateUrl: './twitter-updates.component.html',
  styleUrls: ['./twitter-updates.component.scss']
})
export class TwitterUpdatesComponent implements OnInit{
    title: string = 'twitter updated'
    buttonText: string = 'follow us';

    tweets!: Tweet[];

    ngOnInit(): void {
        this.tweets = [
            {
                Title: 'Live from Apple\'s iPhone OS 4 event!',
                Link: 'http://goo.gle/eferwf',
                PostDate: 'about 1 hour ago'
            },
            {
                Title: 'Reading `50 New Free High-Quality Icon Sets (with Easter icons!)',
                Link: 'http://goo.gle/sdfsdf',
                PostDate: 'about 1 hour ago'
            },
            {
                Title: 'Photoshop tutorial - creating green grass',
                Link: 'http://goo.gle/sdfsdf',
                PostDate: 'about 1 hour ago'
            }
        ]
    }
}
