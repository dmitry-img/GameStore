import {Component, OnInit} from '@angular/core';
import {Article} from "../../../models/Article";

@Component({
  selector: 'app-from-the-blog',
  templateUrl: './from-the-blog.component.html',
  styleUrls: ['./from-the-blog.component.scss']
})
export class FromTheBlogComponent implements OnInit{
    title: string = 'from the blog'
    buttonText: string = 'visit blog';
    articles!: Article[];

    ngOnInit(): void {
        this.articles = [
            {
                Title: 'Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium.',
                Link: 'https://some.link'
            },
            {
                Title: 'Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium.',
                Link: 'https://some.link'
            },
            {
                Title: 'Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium.',
                Link: 'https://some.link'
            },
            {
                Title: 'Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium.',
                Link: 'https://some.link'
            }
        ]
    }
}
