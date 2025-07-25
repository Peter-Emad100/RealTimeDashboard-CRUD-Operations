# 🖥️ Real-Time Collaborative Dashboard
A web dashboard where multiple users can insert, update, delete, or clear items collaboratively in real-time using ASP.NET Core SignalR.

---

## 🚀 Features
- ✅ Insert new items (Name, Price, Quantity)
- ✅ Update existing items dynamically
- ✅ Delete specific items
- ✅ Clear all items instantly
- ✅ Real-time synchronization across all connected clients

---

## 📦 Tech Stack
- Backend: ASP.NET Core 8, SignalR
- Frontend: HTML, CSS, JavaScript
- Communication: WebSockets via SignalR
- Data Store: In-memory dictionary (for demonstration)

---

## 🛠️ Installation & Running
1. Clone the repository
2. Run the application
3. Open in your browser  
   Visit: https://localhost:7110
4. Test Real-Time  
   Open the dashboard in two browser tabs and observe insert/update/delete/clear actions synchronizing live.

---

## 📬 WebSocket Message Structure
All client-server communication follows this JSON structure:
```json
{
  "action": "insert" | "update" | "delete" | "clear",
  "payload": { ... }
}
```
## 🔖 Example Actions
| Action  | Payload Example |
|---------|-----------------|
| insert  | { "name": "Mouse", "price": 20, "quantity": 2 } |
| update  | { "id": "abc-123", "price": 18 } |
| delete  | { "id": "abc-123" } |
| clear   | {} |

---

## ⚙️ Generalized Message Handling (Backend)
All messages are processed via a single hub method that:
1. Deserializes incoming JSON into a DashboardMessage
2. Uses switch-case on action to perform:
   - Insert: Adds new item with generated Guid ID
   - Update: Modifies existing item fields by ID
   - Delete: Removes item by ID
   - Clear: Removes all items
3. Broadcasts updated items list to all connected clients via `Clients.All.SendAsync("ReceiveItems", items.Values)`

---

## 📝 Notes
- This project uses in-memory data storage for demonstration. For production, integrate with a persistent database.
- Frontend uses vanilla JS to focus on real-time logic clarity without framework complexity.
