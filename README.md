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
## 🧠 How It Works (Message Handling Logic)

All incoming messages are handled through a single SignalR hub method that:

1. **Deserializes** incoming JSON into a `DashboardMessage` object.
2. **Delegates logic** to an `ActionDispatcher`, which:
   - Uses a registry of handlers (`IMessageActionHandler`) mapped to each action (e.g., insert, update, delete, clear).
   - Automatically routes the message to the correct handler based on the action type.
3. Each handler performs its specific logic:
   - `InsertHandler`: Adds a new item with a generated `Guid` ID.
   - `UpdateHandler`: Updates existing item fields based on the payload and ID.
   - `DeleteHandler`: Deletes item by ID.
   - `ClearHandler`: Clears all items.
4. After processing, the updated item list is **broadcast to all connected clients** using:

```csharp
Clients.All.SendAsync("ReceiveItems", items.Values);
```


---

## 📝 Notes
- This project uses in-memory data storage for demonstration. For production, integrate with a persistent database.
- Frontend uses vanilla JS to focus on real-time logic clarity without framework complexity.
