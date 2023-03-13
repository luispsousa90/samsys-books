import Book from './Book';

type BookCreate = Omit<Book, 'id'>;

export default BookCreate;
