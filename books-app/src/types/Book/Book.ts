export default interface Book {
  id: string;
  isbn: number;
  name: string;
  authorId: string;
  authorName: string;
  price: number;
  isDeleted: boolean;
}
