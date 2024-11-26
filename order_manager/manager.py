from database.db_manager import DBManager
from utils.logger import logger

class OrderManager:
    def __init__(self, engine, brokers):
        logger.info('Initializing OrderManager')
        self.engine = engine
        self.db_manager = DBManager(engine)
        self.brokers = brokers

    async def reconcile_orders(self, orders):
        logger.info('Reconciling orders', extra={'orders': orders})
        for order in orders:
            await self.reconcile_order(order)
        # Commit the transaction

    async def reconcile_order(self, order):
        logger.info(f'Reconciling order {order.id}', extra={'order_id': order.id, 'broker': order.broker, 'symbol': order.symbol, 'quantity': order.quantity, 'price': order.price, 'side': order.side, 'status': order.status})
        broker = self.brokers[order.broker]
        # TODO: handle partial fill
        filled = await broker.is_order_filled(order)
        if filled:
            try:
                await self.db_manager.set_trade_filled(order.id)
            except Exception as e:
                logger.error(f'Error reconciling order {order.id}', extra={'error': str(e)})

    async def run(self):
        logger.info('Running OrderManager')
        orders = await self.db_manager.get_open_trades()
        await self.reconcile_orders(orders)

async def run_order_manager(engine, brokers):
    order_manager = OrderManager(engine, brokers)
    await order_manager.run()