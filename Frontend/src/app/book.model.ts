export interface Book {
  id: number;
  title: string;
  author: string;
  publishedDate: string;
  price: number;
  isAvailable: boolean;
}

export type CreateBookDto = Omit<Book, 'id'>;
