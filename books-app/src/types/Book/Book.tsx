export default interface Book {
  id: number;
  isbn: number;
  name: string;
  authorId: number;
  authorName: string;
  price: number;
  isDeleted: boolean;
}
