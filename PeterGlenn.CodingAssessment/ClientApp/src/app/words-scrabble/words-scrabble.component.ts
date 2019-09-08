import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-words-scrabble',
  templateUrl: './words-scrabble.component.html'
})
export class WordsScrabbleComponent {
  public scrabbleWords: string[];

  http: HttpClient;
  baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  getScrabbleWords(word: string) {
    this.scrabbleWords = null;
    this.http.get<string[]>(this.baseUrl + 'api/Words/MatchingWordsWithScrabbleValues/' + word).subscribe(result => {
      this.scrabbleWords = result;
    }, error => console.error(error));
  }
}
