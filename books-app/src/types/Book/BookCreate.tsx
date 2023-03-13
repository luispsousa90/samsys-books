import Book from './Book';

type BookCreate = Omit<Book, 'id' | 'authorName'>;

export default BookCreate;
