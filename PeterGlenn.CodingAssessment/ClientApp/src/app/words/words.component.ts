import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-words',
  templateUrl: './words.component.html'
})
export class WordsComponent {
  public matchingWords: string[];

  http: HttpClient;
  baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  getMatchingWords(word: string) {
    this.matchingWords = null;
    this.http.get<string[]>(this.baseUrl + 'api/Words/MatchingWords/' + word).subscribe(result => {
      this.matchingWords = result;
    }, error => console.error(error));
  }
}
